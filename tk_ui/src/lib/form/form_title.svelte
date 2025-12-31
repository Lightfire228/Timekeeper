<script lang="ts">
  export type FormTitleProps = {
    title:        string,
    description:  string,
  };

  let {
    title,
    description,
  }: FormTitleProps = $props();

</script>



<h2 class="text-base/7 font-semibold text-white">
  {title}
</h2>

<p class="mt-1 text-sm/6 text-gray-400">
  {description}
</p>
