<script lang="ts">
    import { resolve } from "$app/paths";
    import type { Pathname } from "$app/types";
    import type { RouteId } from "../../routes/$types";

    export type SidebarEntryProps = {
      display: string,
      path:    RouteId | Pathname,
    };

    let {
      display,
      path,
    }: SidebarEntryProps = $props();


</script>


<li>
  <a href={resolve(path)}>
      <div class="flex flex-col">
          <div class="flex-1">
              {display}
          </div>
      </div>
  </a>
</li>
