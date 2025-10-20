package tk.android.timekeeper.pages

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.activity.enableEdgeToEdge
import androidx.compose.foundation.layout.*
import androidx.compose.material3.Scaffold
import androidx.compose.material3.Text
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.tooling.preview.Preview
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.res.colorResource
import tk.android.timekeeper.ui.theme.TimekeeperTheme
import tk.android.timekeeper.NavbarPages
import tk.android.timekeeper.KDataService
import tk.android.timekeeper.KTaskModel
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.Image
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.unit.dp
import androidx.compose.foundation.border
import androidx.compose.material3.*
import androidx.compose.material.TextField
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.clickable
import androidx.compose.foundation.text.input.rememberTextFieldState
import androidx.compose.runtime.*
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.animateContentSize
import androidx.compose.material.icons.*
import androidx.compose.material.icons.filled.*
import kotlinx.coroutines.launch
import java.util.Calendar
import java.util.Date



@Composable
fun NewTask(data: KDataService) {

    var newTask by remember { mutableStateOf(NewTask()) }

    NewTaskForm(newTask)

}



data class NewTask(
    val name:        String   = "",
    val description: String   = "",
    val priority:    Priority = Priority.None,
    val isDue:       Boolean  = false,
    val dueDate:     Date     = Date(),
)

enum class Priority {
    None,
    Low,
    Medium,
    High,
    FUCKEN_HIGH,
}

@Composable
fun NewTaskForm(newTask: NewTask) {

    Row {
        TextField(
            state = rememberTextFieldState(initialText = ""),
            label = { Text("Task Name") }
        )
    }

}
