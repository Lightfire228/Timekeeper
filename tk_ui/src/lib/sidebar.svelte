<script lang="ts">
  import { afterNavigate } from "$app/navigation";
  import { resolve } from "$app/paths";
  import { Calendar, File, FolderOpen, Home, Menu, PieChart, Team } from "./icons.svelte";
  import SidebarEntry, { type NavEntry } from "./sidebar/sidebar_entry.svelte";
  import SidebarFooter from "./sidebar/sidebar_footer.svelte";
  import SidebarLogo from "./sidebar/sidebar_logo.svelte";

  let { children } = $props();

  const entries: Array<NavEntry> = $state([
    { id: 1, name: 'Tasks',     href: '/',         icon: Home,       current: true  },
    { id: 2, name: 'New Task',  href: '/new_task', icon: Team,       current: false },
    { id: 3, name: 'Projects',  href: '/',         icon: FolderOpen, current: false },
    { id: 4, name: 'Calendar',  href: '/',         icon: Calendar,   current: false },
    { id: 5, name: 'Documents', href: '/',         icon: File,       current: false },
    { id: 6, name: 'Reports',   href: '/',         icon: PieChart,   current: false },
  ]);


  afterNavigate((navigation) => {

    // "debounce" any links that point to the same thing
    let found = false;

    entries.forEach(e => {
      e.current = !found && navigation.to?.route.id == e.href;

      found ||= e.current;
    });
  })


  // TODO:
  let sidebarOpen = $state(true);

</script>


<div>
  <div class="
    hidden bg-gray-900 lg:fixed lg:inset-y-0 lg:z-50 lg:flex lg:w-72 lg:flex-col
  ">
    <div class="
      flex grow flex-col gap-y-5 overflow-y-auto border-r border-white/10 bg-black/10 px-6
    ">

      <SidebarLogo />

      <nav class="flex flex-1 flex-col">
        <ul role="list" class="flex flex-1 flex-col gap-y-7">

          <li>
            <ul role="list" class="flex flex-1 flex-col gap-y-7">
              <li>
                <ul role="list" class="-mx-2 space-y-1">

                  {#each entries as item (item.id)}
                    <SidebarEntry {item} />
                  {/each}

                </ul>
              </li>
            </ul>
          </li>

          <li class="-mx-6 mt-auto">
            <SidebarFooter />
          </li>

        </ul>
      </nav>
    </div>
  </div>

  <!-- condense the sidebar on small screens -->
  <div class="
    sticky top-0 z-40 flex items-center gap-x-6 bg-gray-900 px-4 py-4 after:pointer-events-none
    after:absolute after:inset-0 after:border-b after:border-white/10 after:bg-black/10
    sm:px-6 lg:hidden
  ">
    <button
      type="button"
      onclick={() => sidebarOpen = true}
      class="-m-2.5 p-2.5 text-gray-400 hover:text-white lg:hidden"
    >
      <span class="sr-only">Open sidebar</span>
      {@render Menu()}
    </button>
    <div class="flex-1 text-sm/6 font-semibold text-white">Dashboard</div>
    <a href={resolve('/')}>
      <span class="sr-only">Your profile</span>
      <img
        alt=""
        src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
        class="size-8 rounded-full bg-gray-800 outline -outline-offset-1 outline-white/10"
      />
    </a>
  </div>

  <main class="py-10 lg:pl-72">
    <div class="px-4 sm:px-6 lg:px-8">
      {@render children()}
    </div>
  </main>
</div>
