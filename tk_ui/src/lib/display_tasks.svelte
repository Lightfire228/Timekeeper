<script lang="ts">
    import { goto } from "$app/navigation";
    import { resolve } from "$app/paths";
  import { type Task } from "$lib/types/task";

  export type DisplayTasksProps = {
    tasks: Task[],
  };

  let { tasks }: DisplayTasksProps = $props();




</script>


<div class="px-4 sm:px-6 lg:px-8 pt-4">

  <div class="sm:flex sm:items-center">
    <div class="sm:flex-auto">
      <h1 class="text-base font-semibold text-white">Tasks</h1>
      <p class="mt-2 text-sm text-gray-300">
        A list of all your tasks
      </p>
    </div>

    <div class="mt-4 sm:mt-0 sm:ml-16 sm:flex-none">
      <button
        onclick={() => goto(resolve("/new_task"))}
        type="button"
        class="block rounded-md bg-slate-600 px-3 py-2 text-center text-sm font-semibold text-white shadow-xs hover:bg-slate-400 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-slate-500"
      >
        Add Task
      </button>
    </div>
  </div>

  <div class="mt-8 flow-root">
    <div class="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
      <div class="inline-block min-w-full py-2 align-middle">

        <table class="relative min-w-full divide-y divide-white/15">

          <thead>
            <tr>
              <th
                scope="col"
                class="py-3.5 pr-3 pl-4 text-left text-sm font-semibold text-white sm:pl-6 lg:pl-8"
              >
                Name
              </th>
              <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white">
                Description
              </th>
              <th scope="col" class="px-3 py-3.5 text-left text-sm font-semibold text-white">
                <!-- Email -->
              </th>
              <th scope="col" class="px-3 py-3.5 text-right text-sm font-semibold text-white">
                Id
              </th>
              <th scope="col" class="py-3.5 pr-4 pl-3 sm:pr-6 lg:pr-8">
                <span class="sr-only">Edit</span>
              </th>
            </tr>
          </thead>

          <tbody class="divide-y divide-white/10 bg-zinc-950/">
            {#each tasks as task (task.id)}

              <tr>
                <td class="py-4 pr-3 pl-4 text-sm font-medium whitespace-nowrap text-white sm:pl-6 lg:pl-8">
                  {task.name}
                </td>
                <td class="px-3 py-4 text-sm whitespace-nowrap text-gray-400">           {task.description}</td>
                <td class="px-3 py-4 text-sm whitespace-nowrap text-gray-400">      <!-- {task.description} --></td>
                <td class="px-3 py-4 text-sm whitespace-nowrap text-gray-400 text-right">{task.id} </td>
                <td class="py-4 pr-4 pl-3 text-right text-sm font-medium whitespace-nowrap sm:pr-6 lg:pr-8">
                  <a href="#" class="text-slate-400 hover:text-slate-300">
                    Edit
                    <span class="sr-only">, {task.name}</span>
                  </a>
                </td>
              </tr>

            {/each}

          </tbody>
        </table>

      </div>
    </div>
  </div>

</div>



<!-- <div class="m-3">
    <div class="grid grid-cols-100 gap-1">

        <div class="col-span-2" >                  </div>
        <div class="col-span-5" ><b>Id         </b></div>
        <div class="col-span-25"><b>Name       </b></div>
        <div class="col-span-68"><b>Description</b></div>

        {#each tasks as task (task.id)}
            <div class="col-span-2" >{@render Trash()} </div>
            <div class="col-span-5" >{task.id}         </div>
            <div class="col-span-25">{task.name}       </div>
            <div class="col-span-68">{task.description}</div>
        {/each}
    </div>
</div>
 -->

<!-- <div class="table-auto">
    <table>
        <thead>
            <tr>
                <th>           </th>
                <th>Id         </th>
                <th>Name       </th>
                <th>Description</th>
            </tr>
        </thead>
        <tbody>
            {#each tasks as task (task.id)}
                <tr>
                    <td class="p-0.5">{@render Trash()}</td>
                    <td class="p-0.5">{task.id}         </td>
                    <td class="p-0.5">{task.name}       </td>
                    <td class="p-0.5">{task.description}</td>
                </tr>
            {/each}
        </tbody>
    </table>
</div>
 -->
