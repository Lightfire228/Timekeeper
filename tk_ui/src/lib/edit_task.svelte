<script lang="ts">
    import type { TaskInput } from "./types/task";

    export type EditTaskProps = {
        task:        TaskInput;
        button_text: string;
        on_save:     () => Promise<void>;
    };

    let { 
        task,
        button_text,
        on_save, 
    }: EditTaskProps = $props();

</script>

<div>
    <div>
        <label>
            Task Name:
            <input
                bind:value={task.name}
                autocomplete="off"
            />
        </label>
    </div>
    <div>
        <label>
            Task Description:
            <input
                bind:value={task.description}
                autocomplete="off"
            />
        </label>
    </div>
    <button onclick={on_save}>{button_text}</button>
</div>
