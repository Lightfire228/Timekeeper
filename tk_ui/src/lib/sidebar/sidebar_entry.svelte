<script lang="ts">
    import { resolve } from "$app/paths";
    import type { RouteId } from "$app/types";
    import type { Icon } from "$lib/icons.svelte";


  export type NavEntry = {
    id:      number
    name:    string,
    href:    RouteId,
    icon:    Icon,
    current: boolean
  };

  export type SidebarEntryProps = {
    item: NavEntry,
  };

  let {
    item,
  }: SidebarEntryProps = $props();

</script>


<li>
  <a
    href={resolve(item.href)}
    class={[
      item.current
        ? 'bg-white/5 text-white'
        : 'text-gray-400 hover:bg-white/5 hover:text-white',
      'group flex gap-x-3 rounded-md p-2 text-sm/6 font-semibold',
    ]}
  >
    {@render item.icon([
      'aria-hidden size-6 shrink-0',
      item.current ? 'text-white' : 'text-gray-400 group-hover:text-white',
    ])}
    {item.name}
  </a>
</li>
