<script lang="ts">
    import { resolve } from "$app/paths";


</script>

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
