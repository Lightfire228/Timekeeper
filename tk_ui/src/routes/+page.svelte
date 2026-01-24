<script lang="ts">
    import * as api from "$lib/api";
    import DisplayTasks from "$lib/display_tasks.svelte";
    import EditTask from "$lib/edit_task.svelte";
    import { blank_task, type Task } from "$lib/types/task";

    let tasks: Task[] = $state([])

    let new_task = $state(blank_task());

    const refreshTasks = async () => {
      tasks = await api.get_tasks();
    }

    const onDelete = async (task: Task) => {
      // TODO: confirmation dialog

      await api.delete_task(task.id);

      await refreshTasks();
    }

    refreshTasks();

    const onNewTask = async () => {
      await api.new_task(new_task);

      await refreshTasks();
      new_task = blank_task();
    };
</script>


<main class="">
  <div class="xl:pr-150">
    <div class="px-4 py-10 sm:px-6 lg:px-8 lg:py-6">

      <DisplayTasks {tasks} {onDelete} />

    </div>
  </div>
</main>

<aside class="
  fixed inset-y-0 right-0 hidden w-150 overflow-y-auto border-l border-white/10
  px-4 py-6 sm:px-6 lg:px-8 xl:block
">

  <EditTask
    bind:task = {new_task}
    onSave    = {onNewTask}
    formTitle = "New Task"
    formDesc  = "New Task lorem ipsum dolor sut"
  />

</aside>
