<script lang="ts">
    import SidebarEntry from './sidebar_entry.svelte';

</script>

<ul class="p-2">
    <SidebarEntry
        display = "Tasks"
        path    = "/"
    />
    <SidebarEntry
        display = "New Task"
        path    = "/new_task"
    />
</ul>
