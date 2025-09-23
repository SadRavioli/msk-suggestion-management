<template>
  <button 
    class="btn btn-primary fab-button"
    @click="$emit('click')"
    type="button">
    <i class="bi bi-plus-lg"></i>
  </button>
</template>

<script>
export default {
  name: 'FloatingActionButton',
  emits: ['click']
}
</script>

<style scoped>
.fab-button {
  position: fixed;
  bottom: 2rem;
  right: 2rem;
  width: 60px;
  height: 60px;
  border-radius: 50%;
  border: none;
  box-shadow: 0 4px 12px rgba(0,0,0,0.15);
  z-index: 1000;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.5rem;
}

.fab-button:hover {
  transform: scale(1.1);
  box-shadow: 0 6px 16px rgba(0,0,0,0.2);
}
</style>