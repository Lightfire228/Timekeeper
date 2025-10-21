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
import tk.android.timekeeper.pages.NewTask
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
import androidx.compose.ui.text.TextStyle
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.clickable
import androidx.compose.foundation.text.input.rememberTextFieldState
import androidx.compose.foundation.text.input.TextFieldLineLimits
import androidx.compose.foundation.text.input.TextFieldState
import androidx.compose.runtime.*
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.animateContentSize
import androidx.compose.material.icons.*
import androidx.compose.material.icons.filled.*
import androidx.compose.material3.RadioButton
import kotlinx.coroutines.launch
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.padding
import androidx.compose.foundation.selection.selectable
import androidx.compose.foundation.selection.selectableGroup
import androidx.compose.material3.MaterialTheme
import androidx.compose.material3.RadioButton
import androidx.compose.material3.Text
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.semantics.Role
import androidx.compose.ui.unit.dp
import androidx.compose.material.icons.*
import androidx.compose.material.icons.filled.*
import androidx.compose.material.icons.outlined.*
import java.util.Calendar
import java.util.Date



@Composable
fun NewTask(data: KDataService) {

    NewTaskForm(data)

}

enum class Priority(val display: String) {
    None       ("None"),
    Low        ("Low"),
    Medium     ("Medium"),
    High       ("High"),
    FUCKEN_HIGH("Fucken High"),
}

@Composable
fun NewTaskForm(data: KDataService) {

    val taskName = rememberTextFieldState(initialText = "")
    val taskDesc = rememberTextFieldState(initialText = "")

    Column {
        TextInput(
            label      = "Task Name",
            state      = taskName,
            lineLimits = TextFieldLineLimits.Default
        )
        TextInput(
            label = "Task Description",
            state = taskDesc,
        )

        val radioOptions = Priority.values().reversed()
        val (selectedOption, onOptionSelected) = remember { mutableStateOf(radioOptions.last()) }

        Text("Priority")
        Column(modifier = Modifier.selectableGroup()) {
            radioOptions.forEach { opt ->
                Row(
                    verticalAlignment = Alignment.CenterVertically,
                    modifier          = Modifier
                        .fillMaxWidth()
                        .height      (             56.dp)
                        .padding     (horizontal = 16.dp)
                        .selectable  (
                            selected = ( opt == selectedOption ),
                            onClick  = { onOptionSelected(opt) },
                            role     = Role.RadioButton,
                        )
                    ,
                ) {
                    RadioButton(
                        selected = (opt == selectedOption),
                        onClick  = null, // null recommended for accessibility with screen readers
                    )
                    Text(
                        text     = opt.display,
                        style    = MaterialTheme.typography.bodyLarge,
                        modifier = Modifier.padding(start = 16.dp),
                    )
                }
            }

            Button(
                onClick = {
                    data.onNewTask(KTaskModel(
                        id          = -1,
                        name        = taskName.text.toString(),
                        description = taskDesc.text.toString(),
                        priority    = selectedOption.ordinal,
                        due         = -1,
                    ))
                }
            ) {
                Icon(Icons.Filled.Add, contentDescription = "")
                Spacer(modifier = Modifier.height(3.dp))

                Text("Save")
            }
        }
    }
}


@Composable
fun TextInput(
    label:      String,
    state:      TextFieldState,
    lineLimits: TextFieldLineLimits = TextFieldLineLimits.Default
) {
    val textColor = MaterialTheme.colorScheme.onSurface
    val hintColor = MaterialTheme.colorScheme.onSurface.copy(alpha = 0.70f)

    TextField(
        state       = state,
        textStyle   = TextStyle(color = textColor),
        placeholder = { Text(label, color = hintColor) },
        lineLimits  = lineLimits,
    )
}

