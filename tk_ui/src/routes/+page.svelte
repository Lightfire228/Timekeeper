<script>
    import * as api from "$lib/api.ts";

    let tasks = $state([]);

    api.getTasks().then(res => tasks = res).catch(err => console.log(`err: {err}`));
</script>

<h1>Welcome to SvelteKit</h1>
<p>Visit <a href="https://svelte.dev/docs/kit">svelte.dev/docs/kit</a> to read the documentation</p>


