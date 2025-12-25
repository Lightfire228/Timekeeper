<script lang="ts">
    import * as api from "$lib/api";

    import { blank_task, type TaskInput } from "$lib/types/task";

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

<label>
    Task Name:
    <input
        bind:value={task.name}
        autocomplete="off"
    />
</label>
<label>
    Task Description:
    <input
        bind:value={task.description}
        autocomplete="off"
    />
</label>
<button onclick={new_task}>new task</button>
