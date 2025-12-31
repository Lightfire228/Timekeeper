<script lang="ts">
  export type TextFieldProps = {
    label:       string,
    value:       string,
  };

  let {
    label,
    value = $bindable(''),
  }: TextFieldProps = $props();

</script>

<label for={label} class="block text-sm/6 font-medium text-white">
  {label}
</label>

<div class="mt-2">
  <textarea
    bind:value

    id    = {label}
    name  = {label}
    rows  = {3}
    class = "
      block w-full rounded-md bg-white/5 px-3 py-1.5 text-base text-white outline-1
      -outline-offset-1 outline-white/10 placeholder:text-gray-500 focus:outline-2
      focus:-outline-offset-2 focus:outline-indigo-500 sm:text-sm/6
    "
  ></textarea>
</div>
