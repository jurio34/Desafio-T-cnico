import axios from "axios"

const api = axios.create({
    baseURL: 'http://localhost:5084/api'
})

export default api;