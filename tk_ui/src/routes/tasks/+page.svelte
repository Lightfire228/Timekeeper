<script lang="ts">
  import { invoke } from "@tauri-apps/api/core";

  type TaskInput = {
    name:        string,
    description: string,
  }

  let task: TaskInput = $state({
    name:        "",
    description: "",
  });

  async function newTask(event: Event) {
    event.preventDefault();

    await invoke("new_task", { task })
  }

  async function printDb(event: Event) {
    event.preventDefault();

    await invoke("print_db", {})
  }


</script>

<main class="container">
  <h1>New Task</h1>

  <div class="row">
    <form class="row" onsubmit={newTask}>
        <input id="task-name" placeholder="Enter a name..."        bind:value={task.name} />
        <input id="task-desc" placeholder="Enter a description..." bind:value={task.description} />
        <button type="submit">Save task</button>
    </form>
  </div>


  <div class="row">
    <button onclick={printDb}>Print db</button>
  </div>

  <div class="row">
    <a href="/tasks/list">Tasks List</a>
  </div>

</main>

