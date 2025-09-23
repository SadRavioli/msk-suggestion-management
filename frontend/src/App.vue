<template>
  <div id="app" class="container">
    <h1>MSK Suggestion Management</h1>

    <div v-if="loadingSuggestions">Loading suggestions...</div>
    <div v-else-if="errorSuggestions">Error: {{ errorSuggestions }}</div>
    <div v-else>
      <FilterControls @filtersChanged="handleFiltersChanged" />
      <SuggestionList :suggestions="suggestions" :filters="currentFilters" />
    </div>

    <FloatActionButton @click="showCreateModal = true"></FloatActionButton>

    <CreateSuggestionModal :show="showCreateModal" 
      @close="showCreateModal = false"
      @created="handleSuggestionCreated" />

    <div v-if="loadingEmployees">Loading employees...</div>
    <div v-else-if="errorEmployees">Error: {{ errorEmployees }}</div>
    <EmployeeTable v-else :employees="employees" />
  </div>
</template>

<script>
import 'bootstrap';
import { employeeService, suggestionService } from './services/apiService.js';
import EmployeeTable from './components/EmployeeTable.vue';
import SuggestionList from './components/SuggestionList.vue';
import FloatActionButton from './components/FloatActionButton.vue';
import CreateSuggestionModal from './components/CreateSuggestionModal.vue';
import FilterControls from './components/FilterControls.vue';

export default {
  name: 'App',
  components: {
    SuggestionList,
    FloatActionButton,
    CreateSuggestionModal,
    EmployeeTable,
    FilterControls
  },
  data() {
    return {
      suggestions: [],
      loadingSuggestions: false,
      errorSuggestions: null,
      showCreateModal: false,
      employees: [],
      loadingEmployees: false,
      errorEmployees: null,
      currentFilters: {
        searchText: '',
        status: ''
      }
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
    },
    async handleSuggestionCreated() {
      this.showCreateModal = false
      await this.fetchSuggestions() // Refresh the list
    },
    handleFiltersChanged(newFilters) {
      this.currentFilters = { ...newFilters };
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