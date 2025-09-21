<template>
  <div id="app" class="container">
    <h1>MSK Suggestion Management</h1>
    <div v-if="loading">Loading employees...</div>
    <div v-else-if="error">Error: {{ error }}</div>
    <EmployeeTable v-else :employees="employees" />
  </div>
</template>

<script>
import 'bootstrap';
import { employeeService } from './services/apiService.js'
import EmployeeTable from './components/EmployeeTable.vue'

export default {
  name: 'App',
  components: {
    EmployeeTable
  },
  data() {
    return {
      employees: [],
      loading: false,
      error: null
    }
  },
  async mounted() {
    await this.fetchEmployees()
  },
  methods: {
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