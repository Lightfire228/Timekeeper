<script lang="ts">
    import * as api from "$lib/api";

    import { blank_task, type TaskInput } from "$lib/types/task";
    import EditTask from "$lib/edit_task.svelte";
    import { goto } from "$app/navigation";
    import { resolve } from "$app/paths";

    let task: TaskInput = $state(blank_task());


    const new_task = async() => {
        await api.new_task(task);
        goto(resolve('/'))
    }

</script>

<EditTask
    {task}
    button_text = "New Task"
    on_save     = {new_task}
/>
