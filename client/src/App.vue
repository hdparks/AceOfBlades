
<template>
  <header>

    <div class="wrapper">
        <h2>Test</h2>
    </div>
  </header>

  <main>
    <button @click="onCall">Call</button>
    <button @click="onRaise">Raise</button>
    <button @click="onFold">Fold</button>
  </main>
</template>
<script setup lang="ts">
    import { io } from 'socket.io-client' 
    import { ref } from 'vue';
    
    const messages  = ref<Array<string>>([]);
    const socket = io();
    socket.on('chat message', (msg) => {
        messages.value.push(msg)
    })

    const onFold = () => socket.emit('fold')
    const onCall = () => socket.emit('call')
    const onRaise = () => socket.emit('raise')
</script>

<style>
</style>
