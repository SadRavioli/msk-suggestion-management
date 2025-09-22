<template>
  <div id="app" class="container">
    <h1>MSK Suggestion Management</h1>
    
    <div v-if="loadingSuggestions">Loading employees...</div>
    <div v-else-if="errorSuggestions">Error: {{ errorSuggestions }}</div>
    <SuggestionTable v-else :suggestions="suggestions" />

    <div v-if="loadingEmployees">Loading employees...</div>
    <div v-else-if="errorEmployees">Error: {{ errorEmployees }}</div>
    <EmployeeTable v-else :employees="employees" />
  </div>
</template>

<script>
import 'bootstrap';
import { employeeService, suggestionService } from './services/apiService.js'
import EmployeeTable from './components/EmployeeTable.vue'
import SuggestionTable from './components/SuggestionTable.vue';

export default {
  name: 'App',
  components: {
    SuggestionTable,
    EmployeeTable
  },
  data() {
    return {
      suggestions: [],
      loadingSuggestions: false,
      errorSuggestions: null,
      employees: [],
      loadingEmployees: false,
      errorEmployees: null
    }
  },
  async mounted() {    
    await this.fetchSuggestions(),
    await this.fetchEmployees()
  },
  methods: {
    async fetchSuggestions() {
      this.loading = true
      try {
        this.suggestions = await suggestionService.getSuggestions()
      } catch (error) {
        this.errorSuggestions = error.message
      } finally {
        this.loadingSuggestions = false
      }
    },
    async fetchEmployees() {
      this.loading = true
      try {
        this.employees = await employeeService.getEmployees()
      } catch (error) {
        this.error = error.message
      } finally {
        this.loading = false
      }
    }
  }
}
</script>

<style>

.container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}
</style>