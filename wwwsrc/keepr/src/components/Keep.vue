<template>
    <div class="Keep col-sm-3">
      <h3>{{keep.name}}</h3>
      <p>{{keep.description}}</p>
    </div>
  </template>
  
  <script>
    export default {
      name: 'Keep',
      props: ['keep'],
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