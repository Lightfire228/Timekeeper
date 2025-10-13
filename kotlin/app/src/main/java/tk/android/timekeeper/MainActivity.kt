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
import tk.android.timekeeper.pages.TaskList
import tk.android.timekeeper.pages.TestPage
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.shape.CircleShape
import androidx.compose.foundation.Image
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.clip
import androidx.compose.ui.unit.dp
import androidx.compose.ui.graphics.vector.ImageVector
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
import androidx.compose.material.icons.outlined.*
import kotlinx.coroutines.launch
import kotlinx.coroutines.CoroutineScope


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
                        Navbar(getDataService())
                    }
                }
            }
        }
    }
}

public abstract class KDataService {
    abstract fun getIcon():  Int;

    abstract fun getTasks(): List<KTaskModel>;

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
fun Navbar(data: KDataService) {

    var drawerState = rememberDrawerState(initialValue = DrawerValue.Closed);
    val scope       = rememberCoroutineScope();

    var currentPage by remember { mutableStateOf(NavbarPages.TaskList) }

    var onClick = fun (p: NavbarPages) {
        currentPage = p
        scope.launch {
            drawerState.close()
        }
    }

    @Composable
    fun DrawerItem(icon: ImageVector, name: String, targetPage: NavbarPages) {
        NavigationDrawerItem(
            label    = { Row {
                Icon  (icon, contentDescription = "")
                Spacer(modifier = Modifier.width(3.dp))
                Text  (name) 
            }},
            selected = currentPage == targetPage,
            onClick  = { onClick(targetPage) },
        )
    }

    ModalNavigationDrawer(
        drawerState   = drawerState,
        drawerContent = {
            ModalDrawerSheet {
                DrawerItem(
                    icon       = Icons.Filled.TaskAlt,
                    name       = "Task List",
                    targetPage = NavbarPages.TaskList,
                )
                    DrawerItem(
                    icon       = Icons.Outlined.Science,
                    name       = "Test Page",
                    targetPage = NavbarPages.TestPage,
                )
            }
        }
    ) {
        Scaffold(
            topBar = { TopBar(scope, drawerState) }
        ) { contentPadding ->
            Surface(modifier = Modifier.padding(contentPadding)) {
                Body(currentPage, data)
            }
        }
    }
}

enum class NavbarPages {
    TaskList,
    TestPage,
}

@Composable
fun TopBar(scope: CoroutineScope, drawerState: DrawerState) {

    fun toggleDrawer() {
        scope.launch {
            drawerState.apply {
                if (isClosed) open() else close()
            }
        }
    }

    Button(
        colors  = ButtonDefaults.buttonColors(
            containerColor = MaterialTheme.colorScheme.surface,
            contentColor   = MaterialTheme.colorScheme.onSurface,
        ),
        onClick = ::toggleDrawer,
    ) {
        Icon(Icons.Filled.Menu, contentDescription = "")
    }
}

@Composable
fun Body(currentPage: NavbarPages, data: KDataService) {
    when (currentPage) {
        NavbarPages.TaskList -> TaskList(data)
        NavbarPages.TestPage -> TestPage(data)
    }
}