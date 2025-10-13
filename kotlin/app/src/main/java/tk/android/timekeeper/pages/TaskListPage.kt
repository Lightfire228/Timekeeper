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
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.foundation.clickable
import androidx.compose.runtime.*
import androidx.compose.animation.animateColorAsState
import androidx.compose.animation.animateContentSize
import androidx.compose.material.icons.*
import androidx.compose.material.icons.filled.*
import kotlinx.coroutines.launch


@Composable
fun TaskList(data: KDataService) {

    var tasks = remember { data.getTasks() }
    var icon  = data.getIcon();

    LazyColumn {
        items(tasks) { x ->
            MessageCard(Message(x.name, x.description), icon)
        }
    }
}



data class Message(val author: String, val body: String)

@Composable
fun MessageCard(msg: Message, icon: Int) {

    Row {
        Image(
            painter            = painterResource(icon),
            contentDescription = "",
            modifier           = Modifier
                .size(40.dp)
                .clip(CircleShape)
                // .border(1.5.dp, MaterialTheme.colorScheme.primary)
        )

        Spacer(modifier = Modifier.width(8.dp))

        // keep track of the message state
        var isExpanded   by remember { mutableStateOf(false) }
        val surfaceColor by animateColorAsState(
            if (isExpanded) MaterialTheme.colorScheme.primary   else MaterialTheme.colorScheme.surface
        )
        val textColor by animateColorAsState(
            if (isExpanded) MaterialTheme.colorScheme.onPrimary else MaterialTheme.colorScheme.onSurface
        )

        Column (modifier = Modifier.clickable { isExpanded = !isExpanded } ) {
            Text (
                text     = msg.author,
                color    = MaterialTheme.colorScheme.secondary,
                style    = MaterialTheme.typography .titleSmall,
                modifier = Modifier.animateContentSize().padding(1.dp),
            )
            
            Spacer(modifier = Modifier.height(4.dp))

            Surface(
                shape           = MaterialTheme.shapes.medium,
                shadowElevation = 1.dp,
                color           = surfaceColor,
            ) {
                Text (
                    text     = msg.body,
                    modifier = Modifier.padding(all = 4.dp),
                    maxLines = if (isExpanded) Int.MAX_VALUE else 1,
                    style    = MaterialTheme.typography.bodyMedium,
                    color    = textColor,
                )
            }
        }
    }

}
