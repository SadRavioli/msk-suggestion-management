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
          <td scope="row">{{ employee.name }}</td>
          <td>{{ employee.department }}</td>
          <td>
            <span :class="getRiskClass(employee.riskLevel)">
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
        'text-danger': riskLevel === 'high',
        'text-warning': riskLevel === 'medium',
        'text-success': riskLevel === 'low'
      }
    }
  }
}
</script>

<style scoped>
</style>