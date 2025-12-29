
<script module lang="ts">
  export {
    Add,
    Minus as Remove,
    Checklist,
    Check,
    Trash,
  };
</script>

{#snippet icon(type: string)}
  <span class="{type} align-middle"></span>
{/snippet}

<!-- https://icon-sets.iconify.design/material-symbols/ -->

{#snippet Add()}
  {@render icon('icon-[material-symbols--add-rounded]')}
{/snippet}

{#snippet Minus()}
  {@render icon('icon-[material-symbols--remove]')}
{/snippet}

{#snippet Checklist()}
  {@render icon('icon-[material-symbols--checklist-rounded]')}
{/snippet}

{#snippet Check()}
  {@render icon('icon-[material-symbols--check-box-outline-rounded]')}
{/snippet}

{#snippet Trash()}
  {@render icon('icon-[material-symbols--delete-rounded]')}
{/snippet}
