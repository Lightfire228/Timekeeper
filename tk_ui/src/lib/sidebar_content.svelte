<script lang="ts">
    import { resolve } from '$app/paths';

</script>

<ul>
    <li>
      <a href={resolve('/')}>
          <div class="flex flex-col">
              <div class="flex-1">
                  tasks
              </div>
          </div>
      </a>
    </li>

    <li>
        <a href={resolve('/new_task')}>
            <div class="flex flex-col">
                <div class="flex-1">
                    new task
                </div>
            </div>
        </a>
    </li>

</ul>
