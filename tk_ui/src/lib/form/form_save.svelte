
<script lang="ts">

  export type FormSaveProps = {
    onSave:   () => Promise<void>;
    onCancel: () => Promise<void>;

  };

  let {
    onSave,
    onCancel,
  }: FormSaveProps = $props();

</script>

<button onclick={onCancel} type="button" class="text-sm/6 font-semibold text-white">
  Cancel
</button>

<button
  onclick={onSave}
  type="submit"
  class="
    rounded-md bg-indigo-500 px-3 py-2 text-sm font-semibold text-white focus-visible:outline-2
    focus-visible:outline-offset-2 focus-visible:outline-indigo-500
  "
>
  Save
</button>
