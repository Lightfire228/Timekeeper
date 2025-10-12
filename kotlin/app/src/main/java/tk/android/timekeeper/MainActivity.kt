package tk.android.timekeeper

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


// @Composable
// fun Main() {
    
// }



abstract class KMainActivity : ComponentActivity() {

    abstract fun getDataService(): KDataService

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        enableEdgeToEdge()
        setContent {
            TimekeeperTheme {
                Scaffold(modifier = Modifier.fillMaxSize()) { innerPadding ->
                    Surface(modifier = Modifier.padding(innerPadding)) {
                        MainContent(getDataService())
                    }
                }
            }
        }
    }
}

public abstract class KDataService {
    abstract fun getIcon(): Int;

    abstract fun getTasks():   List<KTaskModel>;

}

public open class KTaskModel(
    val id:          Long,
    val name:        String,
    val description: String,
    val priority:    Int,
    val due:         String?,
    val createdAt:   String,
)

@Composable
fun MainContent(data: KDataService) {
    Navbar (data)
}


@Composable
fun Navbar(data: KDataService) {

    var drawerState = rememberDrawerState(initialValue = DrawerValue.Closed);
    val scope       = rememberCoroutineScope();

    var page by remember { mutableStateOf(NavbarPages.TaskList) }

    var onClick = fun (p: NavbarPages) {
        page = p
        scope.launch {
            drawerState.close()
        }
    }

    ModalNavigationDrawer(
        drawerState   = drawerState,
        drawerContent = {
            ModalDrawerSheet {
                Text("Drawer title")

                // HorizontalDivider()
                NavigationDrawerItem(
                    label    = { Text(text = "Task List") },
                    selected = page == NavbarPages.TaskList,
                    onClick  = { onClick(NavbarPages.TaskList) }
                )
                NavigationDrawerItem(
                    label    = { Text(text = "Test Page") },
                    selected = page == NavbarPages.TestPage,
                    onClick  = { onClick(NavbarPages.TestPage) }
                )
            }
        }
    ) {
        Scaffold(
            floatingActionButton = {
                ExtendedFloatingActionButton(
                    text = { Text("Show drawer") },
                    icon = { Icon(Icons.Filled.Add, contentDescription = "") },
                    onClick = {
                        scope.launch {
                            drawerState.apply {
                                if (isClosed) open() else close()
                            }
                        }
                    }
                )
            }
        ) { contentPadding ->
            Surface(modifier = Modifier.padding(contentPadding)) {
                when (page) {
                    NavbarPages.TaskList -> TaskList(data)
                    NavbarPages.TestPage -> TestPage(data)
                }
            }
        }
    }
}

enum class NavbarPages {
    TaskList,
    TestPage,
}

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

@Composable
fun TestPage(data: KDataService) {
    Text("test page")
}