<template>
  <div>
    <h2>Suggestions ({{ suggestions.length }})</h2>
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
        <tr v-for="suggestion in suggestions" :key="suggestion.id">
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
            <span :class="getStatusClass(suggestion.status)">
              {{ suggestion.status }}
            </span>
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
            <small>{{ suggestion.notes || '-' }}</small>
          </td>
          <td>
            <small>{{ formatDate(suggestion.dateCreated) }}</small>
          </td>
          <td>
            <small>{{ formatDate(suggestion.dateUpdated) }}</small>
          </td>
          <td>
            <small>{{ suggestion.createdBy || '-' }}</small>
          </td>
          <td>
            <small>{{ suggestion.dateCompleted ? formatDate(suggestion.dateCompleted) : '-' }}</small>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { getRiskClass } from '../utils/sharedUtils.js'

export default {
  name: 'SuggestionTable',
  props: {
    suggestions: {
      type: Array,
      required: true
    }
  },
  methods: {
    getRiskClass(riskLevel) {
        riskLevel = riskLevel.toLowerCase();
      return {
        'text-danger': riskLevel === 'high',
        'text-warning': riskLevel === 'medium',
        'text-success': riskLevel === 'low'
      }
    },
    getStatusClass(status) {
        status = status.toLowerCase();
      return {
        'text-secondary': status === 'pending',
        'text-info': status === 'in-progress',
        'text-success': status === 'completed',
        'text-danger': status === 'overdue'
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
    formatDate(dateString) {
      if (!dateString) return '-';
      return new Date(dateString).toLocaleDateString();
    }
  }
}
</script>

<style scoped>
</style>