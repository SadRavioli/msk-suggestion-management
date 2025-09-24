<template>
  <div id="app">
    <header class="vitruvian text-white py-4 mb-4">
      <div class="container">
        <div class="d-flex align-items-center">
          <img src="/favicon.png" alt="Logo" class="me-3" style="height: 48px; width: 48px;">
          <div>
            <h1 class="mb-0">MSK Suggestion Management</h1>
            <p class="mb-0 text-light">Manage employee office health and safety suggestions</p>
          </div>
        </div>
      </div>
    </header>

    <main class="container">
      <div v-if="loading" class="text-center py-5">
        <div class="spinner-border text-primary" role="status">
          <span class="visually-hidden">Loading...</span>
        </div>
        <p class="mt-2">Loading data...</p>
      </div>

      <div v-else-if="error" class="alert alert-danger" role="alert">
        <h2 class="alert-heading">Error Loading Data</h2>
        <p>{{ error }}</p>
        <button class="btn btn-outline-danger" @click="retryLoad">Try Again</button>
      </div>

      <div v-else>
        <section class="section">
          <FilterControls @filtersChanged="handleFiltersChanged" />
          <SuggestionList :suggestions="suggestions" :filters="currentFilters" />
        </section>

        <section v-if="employees.length > 0" class="section mt-5">
          <EmployeeTable :employees="employees" />
        </section>
      </div>
    </main>

    <FloatActionButton class="vitruvian" @click="showCreateModal = true" />

    <CreateSuggestionModal
      :show="showCreateModal"
      @close="showCreateModal = false"
      @created="handleSuggestionCreated" />
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
      employees: [],
      loading: false,
      error: null,
      showCreateModal: false,
      currentFilters: {
        searchText: '',
        status: ''
      }
    }
  },
  async mounted() {
    await this.loadData()
  },
  methods: {
    async loadData() {
      this.loading = true
      this.error = null

      try {
        const [suggestions, employees] = await Promise.all([
          suggestionService.getSuggestions(),
          employeeService.getEmployees()
        ])

        this.suggestions = suggestions
        this.employees = employees
      } catch (error) {
        this.error = error.message
        console.error('Error loading data:', error)
      } finally {
        this.loading = false
      }
    },
    async retryLoad() {
      await this.loadData()
    },
    async handleSuggestionCreated() {
      this.showCreateModal = false

      try {
        this.suggestions = await suggestionService.getSuggestions()
      } catch (error) {
        console.error('Error refreshing suggestions:', error)
      }
    },
    handleFiltersChanged(newFilters) {
      this.currentFilters = { ...newFilters }
    }
  }
}
</script>

<style>
body {
  background-color: #f8f9fa;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
}

#app {
  min-height: 100vh;
}

.container {
  max-width: 1200px;
}

.vitruvian {
  background-color: #023839;
}

.section {
  background: white;
  border-radius: 8px;
  padding: 2rem;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  margin-bottom: 2rem;
}
</style>