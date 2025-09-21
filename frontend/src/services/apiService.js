import axios from 'axios'

const API_BASE_URL = 'http://localhost:5246/api'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  }
})

export const employeeService = {
  async getEmployees() {
    const response = await apiClient.get('/employees')
    return response.data
  }
}

export const suggestionService = {
  async getSuggestions() {
    const response = await apiClient.get('/suggestions')
    return response.data
  }
}