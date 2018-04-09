<template>
    <div class="Vault col-sm-3">
      <h3>{{vault.name}}</h3>
      <p>{{vault.description}}</p>
    </div>
  </template>
  
  <script>
    export default {
      name: 'Vault',
      props: ['vault'],
      mounted() {
  
      },
      data() {
        return {

        }
      },
      methods: {

      },
      computed: {

      }
    }
  </script>
  
  <!-- Add "scoped" attribute to limit CSS to this component only -->
  <style scoped>
  </style>