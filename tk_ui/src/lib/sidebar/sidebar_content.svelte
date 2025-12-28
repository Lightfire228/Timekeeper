<script lang="ts">
    import SidebarEntry from './sidebar_entry.svelte';
    import * as icons from '$lib/icons';

</script>

<ul class="p-2">
    <SidebarEntry
        label   = "Tasks"
        path    = "/"
        icon    = {icons.Check}
    />
    <SidebarEntry
        label   = "New Task"
        path    = "/new_task"
        icon    = {icons.Add}
    />
</ul>
