
<script module lang="ts">

  import clsx from 'clsx';
  import type { ClassValue } from 'svelte/elements';

  export {
    Add,
    Calendar,
    Check,
    Checklist,
    File,
    FolderOpen,
    Home,
    Minus,
    PieChart,
    Team,
    Trash,
  };

  type Style = Array<ClassValue | null | undefined>;

</script>

{#snippet icon(type, style?: Style)}
  <div>
    <span class={{type, style}}></span>
  </div>
{/snippet}

<!-- https://icon-sets.iconify.design/material-symbols/ -->

{#snippet Add(style?: Style)}
  {@render icon('icon-[material-symbols--add-rounded]',                    style)}
{/snippet}

{#snippet Calendar(style?: Style)}
  {@render icon('icon-[material-symbols--calendar-today-outline-rounded]', style)}
{/snippet}

{#snippet Check(style?: Style)}
  {@render icon('icon-[material-symbols--check-box-outline-rounded]',      style)}
{/snippet}

{#snippet Checklist(style?: Style)}
  {@render icon('icon-[material-symbols--checklist-rounded]',              style)}
{/snippet}

{#snippet File(style?: Style)}
  {@render icon('icon-[material-symbols--file-copy-outline-rounded]',      style)}
{/snippet}

{#snippet FolderOpen(style?: Style)}
  {@render icon('icon-[material-symbols--folder-open-outline-rounded]',    style)}
{/snippet}

{#snippet Home(style?: Style)}
  {@render icon('icon-[material-symbols--home-rounded]',                   style)}
{/snippet}

{#snippet Minus(style?: Style)}
  {@render icon('icon-[material-symbols--remove-rounded]',                 style)}
{/snippet}

{#snippet PieChart(style?: Style)}
  {@render icon('icon-[material-symbols--pie-chart]',                      style)}
{/snippet}

{#snippet Team(style?: Style)}
  {@render icon('icon-[material-symbols--group-rounded]',                  style)}
{/snippet}

{#snippet Trash(style?: Style)}
  {@render icon('icon-[material-symbols--delete-rounded]',                 style)}
{/snippet}
