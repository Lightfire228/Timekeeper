import { invoke } from "@tauri-apps/api/core";
import type { Task, TaskInput } from "./types/task";

export async function test_db() {
    await invoke("test_db", {});
}

export async function print_db() {
    await invoke("print_db", {});
}

export async function newTask(task: TaskInput) {
    await invoke("new_task", { task });
}

export async function getTasks(): Promise<Array<Task>> {
    return await invoke("get_tasks", {});
}
