<script lang="ts">
    import * as api from "$lib/api";
    import DisplayTasks from "$lib/display_tasks.svelte";
    import type { Task } from "$lib/types/task";

    let tasks: Task[] = $state([])

    const refresh_tasks = async () => {
      tasks = await api.get_tasks();
    }

    refresh_tasks();
</script>


<DisplayTasks {tasks}/>
