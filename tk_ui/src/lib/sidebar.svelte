<script lang="ts">
    import SidebarContent from "./sidebar_content.svelte";

    let { children } = $props();
</script>

<div class="flex">
    <div class="flex-1 h-screen">
        <SidebarContent/>
    </div>

    <div class="dark:bg-amber-950 flex-4 h-screen">
        {@render children()}
    </div>
</div>
