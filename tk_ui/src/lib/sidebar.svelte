<script lang="ts">
    import { resolve } from "$app/paths";

    let { children } = $props();


  const navigation = [
    { id: 1, name: 'Dashboard', href: '#', icon: '', current: true },
    { id: 2, name: 'Team',      href: '#', icon: '', current: false },
    { id: 3, name: 'Projects',  href: '#', icon: '', current: false },
    { id: 4, name: 'Calendar',  href: '#', icon: '', current: false },
    { id: 5, name: 'Documents', href: '#', icon: '', current: false },
    { id: 6, name: 'Reports',   href: '#', icon: '', current: false },
  ]
  const teams = [
    { id: 1, name: 'Heroicons',     href: '#', initial: 'H', current: false },
    { id: 2, name: 'Tailwind Labs', href: '#', initial: 'T', current: false },
    { id: 3, name: 'Workcation',    href: '#', initial: 'W', current: false },
  ]

  function clsx(...classes: Array<import("svelte/elements").ClassValue | null | undefined>) {
    return classes.filter(Boolean).join(' ')
  }

  let sidebarOpen = $state(true);

</script>


<div>
  <div class="hidden bg-gray-900 lg:fixed lg:inset-y-0 lg:z-50 lg:flex lg:w-72 lg:flex-col">
    <div class="flex grow flex-col gap-y-5 overflow-y-auto border-r border-white/10 bg-black/10 px-6">
      <div class="flex h-16 shrink-0 items-center">
        <img
          alt="Your Company"
          src="https://tailwindcss.com/plus-assets/img/logos/mark.svg?color=indigo&shade=500"
          class="h-8 w-auto"
        />
      </div>
      <nav class="flex flex-1 flex-col">
        <ul role="list" class="flex flex-1 flex-col gap-y-7">
          <li>
            <ul role="list" class="flex flex-1 flex-col gap-y-7">
              <li>
                <ul role="list" class="-mx-2 space-y-1">
                  {#each navigation as item (item.id)}
                    <li>
                        <!-- href={resolve(item.href)} -->
                      <a
                        href={resolve("/")}
                        class={clsx(
                          item.current
                            ? 'bg-white/5 text-white'
                            : 'text-gray-400 hover:bg-white/5 hover:text-white',
                          'group flex gap-x-3 rounded-md p-2 text-sm/6 font-semibold',
                        )}
                      >
                        <!-- <item.icon
                          aria-hidden="true"
                          class={clsx(
                            item.current ? 'text-white' : 'text-gray-400 group-hover:text-white',
                            'size-6 shrink-0',
                          )}
                        /> -->
                        {item.name}
                      </a>
                    </li>
                  {/each}
                </ul>
              </li>
              <li>
                <div class="text-xs/6 font-semibold text-gray-400">Your teams</div>
                <ul role="list" class="-mx-2 mt-2 space-y-1">
                  {#each teams as team (team.id)}
                    <li>
                        <!-- href={team.href} -->
                      <a
                        href={resolve('/')}
                        class={clsx(
                          team.current
                            ? 'bg-white/5 text-white'
                            : 'text-gray-400 hover:bg-white/5 hover:text-white',
                          'group flex gap-x-3 rounded-md p-2 text-sm/6 font-semibold',
                        )}
                      >
                        <span
                          class={clsx(
                            team.current
                              ? 'border-white/20 text-white'
                              : 'border-white/10 text-gray-400 group-hover:border-white/20 group-hover:text-white',
                            'flex size-6 shrink-0 items-center justify-center rounded-lg border bg-white/5 text-[0.625rem] font-medium',
                          )}
                        >
                          {team.initial}
                        </span>
                        <span class="truncate">{team.name}</span>
                      </a>
                    </li>
                  {/each}
                </ul>
              </li>
            </ul>
          </li>
          <li class="-mx-6 mt-auto">
            <a
              href={resolve('/')}
              class="flex items-center gap-x-4 px-6 py-3 text-sm/6 font-semibold text-white hover:bg-white/5"
            >
              <img
                alt=""
                src="https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80"
                class="size-8 rounded-full bg-gray-800 outline -outline-offset-1 outline-white/10"
              />
              <span class="sr-only">Your profile</span>
              <span aria-hidden="true">Tom Cook</span>
            </a>
          </li>
        </ul>
      </nav>
    </div>
  </div>

  <div class="sticky top-0 z-40 flex items-center gap-x-6 bg-gray-900 px-4 py-4 after:pointer-events-none after:absolute after:inset-0 after:border-b after:border-white/10 after:bg-black/10 sm:px-6 lg:hidden">
    <button
      type="button"
      onclick={() => sidebarOpen = true}
      class="-m-2.5 p-2.5 text-gray-400 hover:text-white lg:hidden"
    >
      <span class="sr-only">Open sidebar</span>
      <!-- <Bars3Icon aria-hidden="true" class="size-6" /> -->
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
