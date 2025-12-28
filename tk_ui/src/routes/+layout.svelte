<script lang="ts">
	import './layout.css';
	import favicon from '$lib/assets/favicon.svg';
    import Sidebar from "$lib/sidebar.svelte";

	let { children } = $props();
</script>

<svelte:head><link rel="icon" href={favicon} /></svelte:head>

 <Sidebar>
     {@render children()}
 </Sidebar>
