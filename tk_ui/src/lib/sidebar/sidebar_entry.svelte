<script lang="ts">
  import { resolve } from "$app/paths";
  import type { Pathname } from "$app/types";
  import type { Snippet } from "svelte";
  import type { RouteId } from "../../routes/$types";

  export type SidebarEntryProps = {
    label: string,
    path:  RouteId | Pathname,
    icon:  Snippet,
  };

  let {
    label: display,
    path,
    icon,
  }: SidebarEntryProps = $props();


</script>


<li>
  <a href={resolve(path)}>
    <div class="flex flex-col">
      <div class="flex-1">
        {@render icon()}
        <span class="align-middle">{display}</span>
      </div>
    </div>
  </a>
</li>
