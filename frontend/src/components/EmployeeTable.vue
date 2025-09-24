<template>
  <div>
    <h2>Employees ({{ employees.length }})</h2>
    <table class="table table-striped table-hover">
      <thead>
        <tr>
          <th scope="col">Name</th>
          <th scope="col">Department</th>
          <th scope="col">Risk Level</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="employee in employees" :key="employee.id">
          <td scope="row">{{ employee.fullName }}</td>
          <td>{{ employee.department }}</td>
          <td>
            <span :class="getRiskClass(employee.riskLevel)" class="badge">
              {{ employee.riskLevel }}
            </span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
export default {
  name: 'EmployeeTable',
  props: {
    employees: {
      type: Array,
      required: true
    }
  },
  methods: {
    getRiskClass(riskLevel) {
        riskLevel = riskLevel.toLowerCase();
      return {
        'bg-danger text-white': riskLevel === 'high',
        'bg-warning text-dark': riskLevel === 'medium',
        'bg-success text-white': riskLevel === 'low'
      }
    }
  }
}
</script>

<style scoped>
</style>