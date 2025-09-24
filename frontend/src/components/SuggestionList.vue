<template>
  <div>
    <h2>Suggestions ({{ filteredSuggestions.length }} of {{ suggestions.length }})</h2>

    <!-- Mobile & Tablet: Cards -->
    <div class="d-block d-xl-none">
      <div v-for="suggestion in filteredSuggestions" :key="suggestion.id" class="card mb-4 shadow-sm border-0">
        <div class="card-body p-4">
          <!-- Header: Employee + Status -->
          <div class="d-flex justify-content-between align-items-start mb-3">
            <div class="employee-info">
              <h3 class="card-title mb-1 fw-bold text-dark h6">{{ suggestion.employee?.fullName }}</h3>
              <div class="text-muted small fw-medium">{{ suggestion.employee?.department }}</div>
              <span :class="getRiskClass(suggestion.employee?.riskLevel)" class="badge" :aria-label="`${suggestion.employee?.riskLevel} risk level`">
                {{ suggestion.employee?.riskLevel }} Risk
              </span>
            </div>
            <select
              class="form-select form-select-sm fw-bold border-0 w-50"
              :class="getStatusClass(suggestion.status)"
              v-model="suggestion.status"
              @change="handleStatusUpdate(suggestion.id, $event.target.value)"
              :aria-label="`Status for ${suggestion.employee?.fullName}'s suggestion`">
              <option v-for="status in statuses" :key="status" :value="status">
                {{ status.replace('_', ' ') }}
              </option>
            </select>
          </div>

          <!-- Description -->
          <div class="bg-light p-3 rounded-3 mb-3 border-start border-4">
            <p class="mb-0 text-dark fw-medium">{{ suggestion.description }}</p>
          </div>

          <!-- Type + Priority Row -->
          <div class="d-flex justify-content-between mb-3">
            <span class="badge bg-secondary rounded-pill px-3 py-2">{{ suggestion.type }}</span>
            <span :class="getPriorityClass(suggestion.priority)" class="badge rounded-pill px-3 py-2" :aria-label="`${suggestion.priority} priority`">
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
                <div class="col-12">
                  <small class="text-muted fw-bold text-uppercase">Notes</small>
                  <div v-if="!isEditingNotes(suggestion.id)" @click="startEditingNotes(suggestion.id)" class="small text-dark notes-display" role="button" tabindex="0" @keydown.enter="startEditingNotes(suggestion.id)" @keydown.space.prevent="startEditingNotes(suggestion.id)" :aria-label="`Edit notes: ${suggestion.notes || 'No notes yet'}`">
                    {{ suggestion.notes || '[Click to edit]' }}
                  </div>
                  <div v-else class="notes-edit">
                    <input
                      type="text"
                      class="form-control form-control-sm mt-1"
                      v-model="editingNotesValues[suggestion.id]"
                      @keyup.enter="saveNotes(suggestion.id)"
                      @keyup.escape="cancelEditingNotes(suggestion.id)"
                      ref="notesInput"
                      :aria-label="`Edit notes for ${suggestion.employee?.fullName}'s suggestion`"
                      placeholder="Enter notes...">
                    <div class="mt-2">
                      <button class="btn btn-sm btn-success me-1" @click="saveNotes(suggestion.id)">Save</button>
                      <button class="btn btn-sm btn-secondary" @click="cancelEditingNotes(suggestion.id)">Cancel</button>
                    </div>
                  </div>
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

    <!-- Desktop Table (1200px+) -->
    <div class="d-none d-xl-block">
      <div class="table-responsive">
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
          <tr v-for="suggestion in filteredSuggestions" :key="suggestion.id">
            <td scope="row">
              {{ suggestion.employee?.fullName }} <br/>
              {{ suggestion.employee?.department }} <br/>
              <span :class="getRiskClass(suggestion.employee?.riskLevel)" class="badge" :aria-label="`${suggestion.employee?.riskLevel} risk level`">
                {{ suggestion.employee?.riskLevel }} Risk
              </span>
            </td>
            <td>{{ suggestion.type }}</td>
            <td>{{ suggestion.description }}</td>
            <td>
              <select
                class="form-select form-select-sm fw-bold border-0"
                style="min-width: 130px;"
                :class="getStatusClass(suggestion.status)"
                v-model="suggestion.status"
                @change="handleStatusUpdate(suggestion.id, $event.target.value)"
                :aria-label="`Status for ${suggestion.employee?.fullName}'s suggestion`">
                <option v-for="status in statuses" :key="status" :value="status">
                  {{ status.replace('_', ' ') }}
                </option>
              </select>
            </td>
            <td>
              <span :class="getPriorityClass(suggestion.priority)" class="badge" :aria-label="`${suggestion.priority} priority`">
                {{ suggestion.priority }}
              </span>
            </td>
            <td>
              <span class="badge bg-info">{{ suggestion.source }}</span>
            </td>
            <td>
              <div v-if="!isEditingNotes(suggestion.id)" @click="startEditingNotes(suggestion.id)" class="notes-display" role="button" tabindex="0" @keydown.enter="startEditingNotes(suggestion.id)" @keydown.space.prevent="startEditingNotes(suggestion.id)" :aria-label="`Edit notes: ${suggestion.notes || 'No notes yet'}`">
                {{ suggestion.notes || '[Click to edit]' }}
              </div>
              <div v-else class="notes-edit">
                <input
                  type="text"
                  class="form-control form-control-sm"
                  v-model="editingNotesValues[suggestion.id]"
                  @keyup.enter="saveNotes(suggestion.id)"
                  @keyup.escape="cancelEditingNotes(suggestion.id)"
                  ref="notesInput"
                  :aria-label="`Edit notes for ${suggestion.employee?.fullName}'s suggestion`"
                  placeholder="Enter notes...">
                <div class="mt-1">
                  <button class="btn btn-sm btn-success me-1" @click="saveNotes(suggestion.id)">Save</button>
                  <button class="btn btn-sm btn-secondary" @click="cancelEditingNotes(suggestion.id)">Cancel</button>
                </div>
              </div>
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
  </div>
</template>

<script>
import { suggestionService } from '@/services/apiService';

export default {
  name: 'SuggestionList',
  props: {
    suggestions: {
      type: Array,
      required: true
    },
    filters: {
      type: Object,
      default: () => ({
        searchText: '',
        status: '',
        riskLevel: ''
      })
    }
  },
  data() {
    return {
      expandedIds: new Set(),
      editingNotes: new Set(),
      editingNotesValues: {},
      statuses: ['Pending', 'In_Progress', 'Completed', 'Overdue']
    }
  },
  computed: {
    filteredSuggestions() {
      let filtered = [...this.suggestions];

      if (this.filters.searchText) {
        const searchLower = this.filters.searchText.toLowerCase();
        filtered = filtered.filter(suggestion =>
          suggestion.description?.toLowerCase().includes(searchLower) ||
          suggestion.employee?.fullName?.toLowerCase().includes(searchLower)
        );
      }

      if (this.filters.status) {
        filtered = filtered.filter(suggestion =>
          suggestion.status === this.filters.status
        );
      }

      if (this.filters.riskLevel) {
        filtered = filtered.filter(suggestion =>
          suggestion.employee?.riskLevel === this.filters.riskLevel
        );
      }

      return filtered;
    }
  },
  methods: {
    getRiskClass(riskLevel) {
      const risk = riskLevel?.toLowerCase();
      return {
        'bg-danger text-white': risk === 'high',
        'bg-warning text-dark': risk === 'medium',
        'bg-success text-white': risk === 'low'
      }
    },
    getStatusClass(status) {
      const s = status?.toLowerCase();
      return {
        'bg-secondary text-white': s === 'pending',
        'bg-warning text-dark': s === 'in_progress',
        'bg-success text-white': s === 'completed',
        'bg-danger text-white': s === 'overdue'
      }
    },
    getPriorityClass(priority) {
      const p = priority?.toLowerCase();
      return {
        'bg-danger text-white': p === 'high',
        'bg-warning text-dark': p === 'medium',
        'bg-success text-white': p === 'low'
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
    },
    isEditingNotes(id) {
      return this.editingNotes.has(id);
    },
    startEditingNotes(id) {
      const suggestion = this.suggestions.find(s => s.id === id);
      this.editingNotesValues[id] = suggestion?.notes || '';
      this.editingNotes.add(id);
      this.$forceUpdate();

      this.$nextTick(() => {
        const inputs = this.$refs.notesInput;
        if (inputs) {
          const input = Array.isArray(inputs) ? inputs.find(inp => inp) : inputs;
          if (input) input.focus();
        }
      });
    },
    cancelEditingNotes(id) {
      this.editingNotes.delete(id);
      delete this.editingNotesValues[id];
      this.$forceUpdate();
    },
    async saveNotes(id) {
      try {
        const newNotes = this.editingNotesValues[id];
        await suggestionService.updateNotes(id, newNotes);

        const suggestion = this.suggestions.find(s => s.id === id);
        if (suggestion) {
          suggestion.notes = newNotes;
        }

        this.editingNotes.delete(id);
        delete this.editingNotesValues[id];
        this.$forceUpdate();

        console.log(`Notes updated for suggestion ${id}`);
      } catch (error) {
        console.error('Error updating notes:', error);
        alert('Failed to update notes. Please try again.');
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

.notes-display {
  cursor: pointer;
  padding: 4px;
  border: 1px dashed #ccc;
}

.notes-display:hover {
  background-color: #f0f0f0;
}

.form-select {
  cursor: pointer;
}

.form-control:focus,
.form-select:focus {
  outline: none;
  box-shadow: none;
}
</style>