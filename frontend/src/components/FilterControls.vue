<template>
  <div class="mb-3">
    <div class="row">
      <div class="col-md-6">
        <label class="form-label">Search</label>
        <input
          type="text"
          class="form-control"
          placeholder="Search suggestions..."
          v-model="searchText"
          @input="updateFilters">
      </div>
      <div class="col-md-3">
        <label class="form-label">Status</label>
        <select class="form-select" v-model="selectedStatus" @change="updateFilters">
          <option value="">All Statuses</option>
          <option value="Pending">Pending</option>
          <option value="In_Progress">In Progress</option>
          <option value="Completed">Completed</option>
          <option value="Overdue">Overdue</option>
        </select>        
      </div>
      <div class="col-md-3">
        <label class="form-label">Risk Level</label>
        <select class="form-select" v-model="selectedRisk" @change="updateFilters">
          <option value="">All Levels</option>
          <option value="High">High</option>
          <option value="Medium">Medium</option>
          <option value="Low">Low</option>
        </select>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'FilterControls',
  data() {
    return {
      searchText: '',
      selectedStatus: '',
      selectedRisk: ''
    }
  },
  methods: {
    updateFilters() {
      this.$emit('filtersChanged', {
        searchText: this.searchText,
        status: this.selectedStatus,
        riskLevel: this.selectedRisk
      });
    }
  }
}
</script>

<style scoped>
.form-control:focus,
.form-select:focus {
  outline: none;
  box-shadow: none;
}
</style>

