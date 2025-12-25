<script lang="ts">
    import { type Task } from "$lib/types/task";

    export type DisplayTasksProps = {
      tasks: Task[],
    };

    let { tasks }: DisplayTasksProps = $props();

</script>

Task:
<table>
    <thead>
        <tr>
            <th>Id         </th>
            <th>Name       </th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
        {#each tasks as task (task.id)}
            <tr>
                <td>{task.id}         </td>
                <td>{task.name}       </td>
                <td>{task.description}</td>
            </tr>
        {/each}
    </tbody>
</table>
