<script lang="ts">
    import SidebarContent from "./sidebar/sidebar_content.svelte";

    let { children } = $props();
</script>

<!-- <div class="flex">
  <div class="flex-1 h-screen bg-zinc-900">
    <SidebarContent/>
  </div>

  <div class="flex-4 h-screen">
    {@render children()}
  </div>
</div> -->

{@render children()}
