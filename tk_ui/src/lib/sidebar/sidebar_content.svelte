<script lang="ts">
  import SidebarEntry from './sidebar_entry.svelte';
  import { Add, Check } from '$lib/icons.svelte';

</script>

<ul class="p-2">
  <SidebarEntry
    label   = "Tasks"
    path    = "/"
    icon    = {Check}
  />
  <SidebarEntry
    label   = "New Task"
    path    = "/new_task"
    icon    = {Add}
  />
</ul>
