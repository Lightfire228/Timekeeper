<script lang="ts">

  import * as api from "$lib/api";

  const onCreateBackup = async () => {
    await api.backup_db();
  };

</script>

<div class="mt-4 sm:mt-0 sm:ml-16 sm:flex-none">
  <button
    onclick={onCreateBackup}
    type="button"
    class="block rounded-md bg-slate-600 px-3 py-2 text-center text-sm font-semibold text-white shadow-xs hover:bg-slate-400 focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-slate-500"
  >
    Create Backup
  </button>
</div>
