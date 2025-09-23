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
  },

  async addSuggestion(suggestion) {
    const response = await apiClient.post('/suggestions', suggestion)
    return response.data
  },

  async updateStatus(suggestionId, status, notes = '') {
    const response = await apiClient.patch(`/suggestions/${suggestionId}/status`, 
      {
        status,
        notes
      })
    return response.data
  }
}