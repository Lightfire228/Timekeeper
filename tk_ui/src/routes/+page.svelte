<script lang="ts">
    import * as api from "$lib/api";
    import DisplayTasks from "$lib/display_tasks.svelte";
    import NewTask from "$lib/new_task.svelte";
    import type { Task } from "$lib/types/task";

    let tasks: Task[] = $state([])

    const refresh_tasks = async () => {
      tasks = await api.get_tasks();
    }

    refresh_tasks();
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://svelte.dev/docs/kit">svelte.dev/docs/kit</a> to read the documentation</p>


<NewTask on_new_task={refresh_tasks}/>
<DisplayTasks {tasks}/>
