<script lang="ts">
  import type { TaskInput } from "./types/task";
    import TextField from "./form/text_field.svelte";
    import FormTitle from "./form/form_title.svelte";
    import TextArea from "./form/text_area.svelte";
    import { goto } from "$app/navigation";
    import { resolve } from "$app/paths";
    import FormSave from "./form/form_save.svelte";

  export type EditTaskProps = {
    task:        TaskInput;
    onSave:      () => Promise<void>;

    formTitle:   string;
    formDesc:    string;
  };

  let {
    task      = $bindable(),
    onSave,

    formTitle = "Edit Task",
    formDesc  = "Edit task page description",
  }: EditTaskProps = $props();

  const onCancel = async () => {
    goto(resolve("/"))
  };



</script>


<!-- https://tailwindcss.com/plus/ui-blocks/application-ui/forms/form-layouts -->

<div class="space-y-12">
  <div class="border-b border-white/10 pb-12">

    <FormTitle
      title       = {formTitle}
      description = {formDesc}
    />

    <div class="mt-10 grid grid-cols-1 gap-x-6 gap-y-8 sm:grid-cols-6">

      <div class="col-span-full">
        <TextField
          label      = "Name"
          bind:value = {task.name}
        />
      </div>

      <div class="col-span-full">
        <TextArea
          label      = "Description"
          bind:value = {task.description}
        />
      </div>

    </div>
  </div>
</div>

<div class="mt-6 flex items-center justify-start gap-x-6">
  <FormSave {onSave} {onCancel} />
</div>
