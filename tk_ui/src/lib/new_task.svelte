<script lang="ts">
    import * as api from "$lib/api";

    import { blank_task, type TaskInput } from "$lib/types/task";
  import EditTask from "./edit_task.svelte";

    let task: TaskInput = $state(blank_task());

    export type NewTaskProps = {
      on_new_task: () => Promise<void>;
    };

    let { on_new_task }: NewTaskProps = $props();

    const new_task = async() => {
      await api.new_task(task);
      task = blank_task();

      on_new_task();
    }

</script>

<EditTask 
    {task}
    button_text = "New Task"
    on_save     = {new_task}
/>

