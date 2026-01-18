<script lang="ts">
    import * as api from "$lib/api";
    import DisplayTasks from "$lib/display_tasks.svelte";
    import type { Task } from "$lib/types/task";

    let tasks: Task[] = $state([])

    const refreshTasks = async () => {
      tasks = await api.get_tasks();
    }

    const onDelete = async (task: Task) => {
      // TODO: confirmation dialog

      await api.delete_task(task.id);

      await refreshTasks();
    }

    refreshTasks();
</script>


<DisplayTasks {tasks} {onDelete} />
