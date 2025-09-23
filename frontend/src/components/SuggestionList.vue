<template>
  <div>
    <h2>Suggestions ({{ suggestions.length }})</h2>

    <!-- Mobile: Cards -->
    <div class="d-block d-md-none">
      <div v-for="suggestion in localSuggestions" :key="suggestion.id" class="card mb-4 shadow-sm border-0">
        <div class="card-body p-4">
          <!-- Header: Employee + Status -->
          <div class="d-flex justify-content-between align-items-start mb-3">
            <div class="employee-info">
              <h6 class="card-title mb-1 fw-bold text-dark">{{ suggestion.employee?.fullName }}</h6>
              <div class="text-muted small fw-medium">{{ suggestion.employee?.department }}</div>
              <span :class="getRiskClass(suggestion.employee?.riskLevel)" class="badge rounded-pill mt-2">
                {{ suggestion.employee?.riskLevel }} Risk
              </span>
            </div>
            <select
              class="form-select form-select-sm w-auto"
              :class="getStatusClass(suggestion.status)"
              v-model="suggestion.status"
              @change="handleStatusUpdate(suggestion.id, $event.target.value)">
              <option v-for="status in statuses" :key="status" :value="status">
                {{ status }}
              </option>
            </select>
          </div>

          <!-- Description -->
          <div class="bg-light p-3 rounded-3 mb-3 border-start border-primary border-4">
            <p class="mb-0 text-dark fw-medium">{{ suggestion.description }}</p>
          </div>

          <!-- Type + Priority Row -->
          <div class="d-flex justify-content-between mb-3">
            <span class="badge bg-secondary rounded-pill px-3 py-2">{{ suggestion.type }}</span>
            <span :class="getPriorityClass(suggestion.priority)" class="badge rounded-pill px-3 py-2">
              {{ suggestion.priority }}
            </span>
          </div>

          <!-- Details section -->
          <div v-if="isExpanded(suggestion.id)" class="mt-3 pt-3 border-top">
            <div class="bg-light p-3 rounded-3">
              <div class="row g-3">
                <div class="col-6">
                  <small class="text-muted fw-bold text-uppercase">Source</small>
                  <div class="badge bg-info rounded-pill">{{ suggestion.source }}</div>
                </div>
                <div class="col-6">
                  <small class="text-muted fw-bold text-uppercase">Created</small>
                  <div class="small text-dark">{{ formatDate(suggestion.dateCreated) }}</div>
                </div>
                <div class="col-12" v-if="suggestion.notes">
                  <small class="text-muted fw-bold text-uppercase">Notes</small>
                  <div class="small text-dark">{{ suggestion.notes }}</div>
                </div>
                <div class="col-12" v-if="suggestion.createdBy">
                  <small class="text-muted fw-bold text-uppercase">Created By</small>
                  <div class="small text-dark">{{ suggestion.createdBy }}</div>
                </div>
              </div>
            </div>
          </div>

          <!-- Toggle Button -->
          <button 
            class="btn btn-link p-0 mt-3 text-decoration-none fw-semibold"
            @click="toggleExpanded(suggestion.id)">
            <i class="bi bi-chevron-down me-1"></i>
            {{ isExpanded(suggestion.id) ? 'Hide Details' : 'Show Details' }}
          </button>
        </div>
      </div>
    </div>

    <!-- Desktop Table -->
    <div class="d-none d-md-block">
      <table class="table table-striped table-hover">
        <thead>
          <tr>
            <th scope="col">Employee Details</th>
            <th scope="col">Type</th>
            <th scope="col">Description</th>
            <th scope="col">Status</th>
            <th scope="col">Priority</th>
            <th scope="col">Source</th>
            <th scope="col">Notes</th>
            <th scope="col">Date Created</th>
            <th scope="col">Date Updated</th>
            <th scope="col">Created By</th>
            <th scope="col">Date Completed</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="suggestion in localSuggestions" :key="suggestion.id">
            <td scope="row">
              {{ suggestion.employee?.fullName }} <br/>
              {{ suggestion.employee?.department }} <br/>
              <span :class="getRiskClass(suggestion.employee?.riskLevel)">
                {{ suggestion.employee?.riskLevel }} Risk
              </span>
            </td>
            <td>{{ suggestion.type }}</td>
            <td>{{ suggestion.description }}</td>
            <td>
              <select
                class="form-select form-select-sm"
                :class="getStatusClass(suggestion.status)"
                v-model="suggestion.status"
                @change="handleStatusUpdate(suggestion.id, $event.target.value)">
                <option v-for="status in statuses" :key="status" :value="status">
                  {{ status }}
                </option>
              </select>
            </td>
            <td>
              <span :class="getPriorityClass(suggestion.priority)" class="badge">
                {{ suggestion.priority }}
              </span>
            </td>
            <td>
              <span class="badge bg-info">{{ suggestion.source }}</span>
            </td>
            <td>
              {{ suggestion.notes || '-' }}
            </td>
            <td>
              {{ formatDate(suggestion.dateCreated) }}
            </td>
            <td>
              {{ formatDate(suggestion.dateUpdated) }}
            </td>
            <td>
              {{ suggestion.createdBy || '-' }}
            </td>
            <td>
              {{ suggestion.dateCompleted ? formatDate(suggestion.dateCompleted) : '-' }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SuggestionList',
  props: {
    suggestions: {
      type: Array,
      required: true
    }
  },
  data() {
    return {
      expandedIds: new Set(),
      localSuggestions: [], 
      statuses: ['Pending', 'In_Progress', 'Completed', 'Overdue']
    }
  },
  watch: {
    suggestions: {
      handler(newSuggestions) {
        this.localSuggestions = JSON.parse(JSON.stringify(newSuggestions));
      },
      immediate: true,
      deep: true
    }
  },
  methods: {
    getRiskClass(riskLevel) {
      const risk = riskLevel?.toLowerCase();
      return {
        'text-danger': risk === 'high',
        'text-warning': risk === 'medium',
        'text-success': risk === 'low'
      }
    },
    getStatusClass(status) {
      const s = status?.toLowerCase().replace(/\s/g, '_');
      return {
        'bg-secondary': s === 'pending',
        'bg-warning': s === 'in_progress',
        'bg-success': s === 'completed',
        'bg-danger': s === 'overdue'
      }
    },
    getPriorityClass(priority) {
      const p = priority?.toLowerCase();
      return {
        'text-danger': p === 'high',
        'text-warning': p === 'medium',
        'text-secondary': p === 'low'
      }
    },
    isExpanded(id) {
      return this.expandedIds.has(id);
    },
    toggleExpanded(id) {
      if (this.expandedIds.has(id)) {
        this.expandedIds.delete(id);
      } else {
        this.expandedIds.add(id);
      }
      this.$forceUpdate();
    },
    formatDate(dateString) {
      if (!dateString) return '-';
      return new Date(dateString).toLocaleDateString();
    },
    async handleStatusUpdate(suggestionId, newStatus) {
      try {
        await suggestionService.updateStatus(suggestionId, newStatus);
        console.log(`Status of suggestion ${suggestionId} updated to ${newStatus}`);
      } catch (error) {
        console.error('Error updating status:', error);
        alert('Failed to update status. Please try again.');
      }
    }
  }
}
</script>

<style scoped>
.card:hover {
  transform: translateY(-2px);
  transition: transform 0.2s ease;
}
</style>