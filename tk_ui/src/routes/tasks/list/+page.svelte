<script lang="ts">
  import { invoke } from "@tauri-apps/api/core";

  let tasks: Array<Task> = $state([]);

  async function getTasks() {
    tasks = await invoke("get_tasks", {}) as Array<Task>;
  }

  getTasks();

  type Task = {
    id:          number,
    name:        string,
    description: string,
  }
</script>

<main class="container">
    <h1>Tasks</h1>

    <div class="row">
        <table>
            <thead>
                <tr>
                    <td>id         </td>
                    <td>name       </td>
                    <td>description</td>
                </tr>
            </thead>

            {#each tasks as task}
                <tbody>
                    <tr>
                        <td>{task.id         }</td>
                        <td>{task.name       }</td>
                        <td>{task.description}</td>
                    </tr>
                </tbody>
            {/each}
        </table>
    </div>
</main>

