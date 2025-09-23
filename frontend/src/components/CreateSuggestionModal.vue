<template>
  <!-- Modal backdrop and container -->
  <div v-if="show" class="modal fade show d-block" tabindex="-1" style="background-color: rgba(0,0,0,0.5);">
    <div class="modal-dialog modal-lg">
      <div class="modal-content">
        
        <!-- Modal header -->
        <div class="modal-header">
          <h5 class="modal-title">Create New Suggestion</h5>
          <button type="button" class="btn-close" @click="closeModal"></button>
        </div>
        
        <!-- Modal body with form -->
        <div class="modal-body">
          <form @submit.prevent="submitForm">
    
            <!-- Employee Selection -->
            <div class="mb-3">
              <label class="form-label">Employee *</label>
              <select class="form-select" v-model="form.employeeId" required>
                <option value="">Select an employee...</option>
                <option v-for="employee in employees" :key="employee.id" :value="employee.id">
                  {{ employee.fullName }} - {{ employee.department }} - {{ employee.riskLevel }} Risk
                </option>
              </select>
            </div>
            
            <!-- Type -->
            <div class="mb-3">
              <label class="form-label">Type *</label>
              <select class="form-select" v-model="form.type" required>
                <option value="">Select type...</option>
                <option value="Equipment">Equipment</option>
                <option value="Exercise">Exercise</option>
                <option value="Behavioural">Behavioural</option>
                <option value="Lifestyle">Lifestyle</option>
              </select>
            </div>

            <!-- Description -->
            <div class="mb-3">
              <label class="form-label">Description *</label>
              <textarea class="form-control" v-model="form.description" rows="3" required 
                        placeholder="Describe the suggestion..."></textarea>
            </div>
            
            <!-- Priority -->
            <div class="mb-3">
              <label class="form-label">Priority *</label>
              <select class="form-select" v-model="form.priority" required>
                <option value="">Select priority...</option>
                <option value="Low">Low</option>
                <option value="Medium">Medium</option>
                <option value="High">High</option>
              </select>
            </div>
            
            <!-- Notes -->
            <div class="mb-3">
              <label class="form-label">Notes</label>
              <textarea class="form-control" v-model="form.notes" rows="2" 
                        placeholder="Additional notes (optional)"></textarea>
            </div>
            
          </form>
        </div>
        
        <!-- Modal footer -->
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" @click="closeModal">Cancel</button>
          <button type="button" class="btn btn-primary" @click="submitForm">Create Suggestion</button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { employeeService, suggestionService } from '../services/apiService.js';

export default {
  name: 'CreateSuggestionModal',
  props: {
    show: {
      type: Boolean,
      default: false
    }
  },
  emits: ['close', 'created'],
  data() {
    return {
      employees: [],
      form: {
        employeeId: '',
        type: '',
        description: '',
        priority: '',
        notes: ''
      }
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
    },

    closeModal() {
      this.resetForm()
      this.$emit('close')
    },

    resetForm() {
      this.form = {
        employeeId: '',
        type: '',
        description: '',
        priority: '',
        notes: ''
      }
    },

    async submitForm() {
      if (!this.form.employeeId || !this.form.type || !this.form.description || !this.form.priority) {
        alert('Please fill in all required fields')
        return
      }

      this.loading = true
      try {
        const suggestion = {
          employeeId: this.form.employeeId,
          type: this.form.type,
          description: this.form.description,
          priority: this.form.priority,
          source: 'Admin',
          notes: this.form.notes || null,
          dateCreated: Date.UTC.Date,
          dateUpdated: Date.UTC.Date,
          createdBy: 'hsmanager@company.com'
        }

        const createdSuggestion = await suggestionService.addSuggestion(suggestion)
        
        this.$emit('created', createdSuggestion)
        this.closeModal()
      } catch (error) {
        console.error('Failed to create suggestion:', error)
        alert('Failed to create suggestion. Please try again.')
      } finally {
        this.loading = false
      }
    }
  }
}
</script>