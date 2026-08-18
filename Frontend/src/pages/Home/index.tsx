import "./style.css";
import Trash from "../../assets/trash-svgrepo-com (1).svg";
import api from "../../services/api";
import { useState, useEffect, useRef } from "react";
function Home() {
  const [reservas, setReservas] = useState<any[]>([]);
  const [salas, setSalas] = useState<any[]>([]);
  const [errorMessage, setErrorMessage] = useState("");

  const inputTitulo = useRef<HTMLInputElement>(null);
  const inputInicio = useRef<HTMLInputElement>(null);
  const inputTermino = useRef<HTMLInputElement>(null);
  const selectSalaId = useRef<HTMLSelectElement>(null);

  async function getReservas() {
    try {
      const response = await api.get("/reserva");
      // 1. Pega a chave 'reservas' e 'salas' do objeto retornado pelo C#
      setReservas(response.data.reservasAgrupadas || []);
      setSalas(response.data.salas || []);
    } catch (error) {
      console.error("Erro ao carregar dados:", error);
    }
  }

  async function createReservas() {
    setErrorMessage(""); // Limpa a mensagem de erro antes de tentar criar uma nova reserva
    // Validação simples antes de tentar ler o .value
    if (
      !inputTitulo.current?.value ||
      !inputInicio.current?.value ||
      !inputTermino.current?.value ||
      !selectSalaId.current?.value
    ) {
      alert("Preencha todos os campos!");
      return;
    }

    try {
      await api.post("/reserva", {
        titulo: inputTitulo.current.value,
        startTime: inputInicio.current.value,
        endTime: inputTermino.current.value,
        salaId: Number(selectSalaId.current.value),
      });

      getReservas();

      // Limpa os campos após enviar
      inputTitulo.current.value = "";
      inputInicio.current.value = "";
      inputTermino.current.value = "";
      selectSalaId.current.value = "";
    } catch (error: any) {
      if (error.response && error.response.data) {
        const data = error.response.data;
        setErrorMessage(
          typeof data === "string" ? data : "Ocorreu um erro no cadastro.",
        );
      } else {
        setErrorMessage("Erro ao conectar com o servidor.");
      }
    }
  }

  async function deleteReservas(id: number) {
    try {
      await api.delete(`/reserva/${id}`);
      getReservas();
    } catch (error) {
      setErrorMessage("Erro ao deletar reserva.");
    }
  }

  useEffect(() => {
    getReservas();
  }, []);

  return (
    <div className="container">
      <form>
        {errorMessage && <div className="error-card">{errorMessage}</div>}
        <h1>Cadastro de Reservas</h1>
        <input
          placeholder="Título"
          name="Titulo"
          type="text"
          ref={inputTitulo}
        />
        <input
          placeholder="Ínicio da Reunião"
          name="Início"
          type="datetime-local"
          ref={inputInicio}
        />
        <input
          placeholder="Término da Reunião"
          name="Término"
          type="datetime-local"
          ref={inputTermino}
        />
        <select
          name="Sala"
          ref={selectSalaId}
          defaultValue=""
          className="select-sala"
        >
          <option value="" disabled>
            Selecione a Sala
          </option>
          {salas?.map((sala) => (
            <option key={sala.id} value={sala.id}>
              {sala.nome}
            </option>
          ))}
        </select>

        <button type="button" onClick={createReservas}>
          Cadastrar
        </button>
      </form>

      {reservas?.map((grupo: any) => (
        <div key={grupo.date} className="grupo-dia">
          <h2 className="titulo-dia">
            {new Date(grupo.date + "T00:00:00").toLocaleDateString("pt-BR")}
          </h2>

          {grupo.reservations?.map((reserva: any) => (
            <div key={reserva.id} className="card">
              <div>
                <p>
                  Título: <span>{reserva.titulo}</span>
                </p>
                <p>
                  Início: <span>{reserva.startTime}</span>
                </p>
                <p>
                  Término: <span>{reserva.endTime}</span>
                </p>
                <p>
                  Nome da sala: <span>{reserva.sala?.nome}</span>
                </p>
              </div>
              <button type="button" onClick={() => deleteReservas(reserva.id)}>
                <img src={Trash} alt="Deletar" />
              </button>
            </div>
          ))}
        </div>
      ))}
    </div>
  );
}

export default Home;
